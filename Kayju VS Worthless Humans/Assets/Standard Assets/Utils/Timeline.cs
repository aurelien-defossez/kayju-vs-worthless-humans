using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class TimelineAction {
    public MonoBehaviour script;
    public string method;
    public float from;
    public float to;
    public int parameter;
    public bool started;
    public bool finished;

    public TimelineAction(MonoBehaviour script, string method) {
        this.script = script;
        this.method = method;
        this.from = 0;
        this.to = 0;
        this.parameter = 0;
    }

    public MethodInfo getMethod() {
        return script.GetType().GetMethod(method);
    }
}

public class TimelineCall {
    public bool firstFrame;
    public bool finishing;
    public float delta;
    public float duration;
    public float progress;
    public int parameter;

    public TimelineCall() { }
}

public class Timeline : MonoBehaviour {
    // ----------------------------------------------------------------------------
    // Configuration
    // ----------------------------------------------------------------------------

    public float duration;
    public bool loop;
    public bool unscaledTime;
    public bool autoPlay;
    public float delay = 0.0f;
    public float maxDeltaTime = 0.15f;

    [HideInInspector]
    public List<TimelineAction> actions;

    // ----------------------------------------------------------------------------
    // Attributes
    // ----------------------------------------------------------------------------

    private bool isPlaying;
    public bool IsPlaying { get { return isPlaying; } }

    private bool finishing;
    private float time;

    // ----------------------------------------------------------------------------
    // Initialization
    // ----------------------------------------------------------------------------

    public void Awake() {
        if (duration <= 0) {
            foreach (TimelineAction action in actions) {
                duration = Mathf.Max(duration, action.to);
            }
        }

        ResetFXs();

        if (autoPlay) {
            Play();
        }
    }

    // ----------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------

    // Start the player
    public void Play() {
        isPlaying = true;
        UpdateFXs();
    }

    // Restart the timeline
    public void Restart() {
        time = 0;
        ResetFXs();
        Play();
    }

    // Stop the player
    public void Stop() {
        isPlaying = false;
        time = 0;
        UpdateFXs();
    }

    // Pause the player
    public void Pause() {
        isPlaying = false;
    }

    // Resume the player
    public void Resume() {
        isPlaying = true;
    }

    // Advance in the timeline
    //
    // Parameters:
    //  time: The time to add to the timeline
    public void Forward(float time) {
        this.time += time;
        UpdateFXs();
    }

    // Finish the timeline immediately
    public void Finish() {
        finishing = true;

        if (time < duration) {
            Forward(duration - time);
        }
    }

    // Check is the timeline is finished
    //
    // Returns true if the timeline is finished
    public bool IsFinished() {
        return duration > 0 && time >= duration;
    }

    public void Update() {
        if (isPlaying) {
            var dt = (unscaledTime) ? Time.unscaledDeltaTime : Time.deltaTime;

            if (dt > 0 && dt < maxDeltaTime) {
                if (delay > 0) {
                    delay -= dt;
                } else {
                    time += dt;

                    UpdateFXs();

                    // Check if it needs to loop
                    if (loop && time >= duration) {
                        Restart();
                    }
                }
            }
        }
    }

    // ----------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------

    private void UpdateFXs() {
        if (delay <= 0) {
            foreach (TimelineAction action in actions) {
                //UnityEngine.Debug.Log("Check action "+action.finished+": "+time+" / "+ action.from+" > "+action.to);
                if (!action.finished && time >= action.from) {
                    var options = new TimelineCall();

                    options.parameter = action.parameter;
                    options.firstFrame = !action.started;
                    options.delta = time - action.from;
                    options.finishing = finishing;
                    options.duration = action.to - action.from;
                    options.progress = (options.duration > 0) ? options.delta / options.duration : 1;

                    if (options.progress >= 1) {
                        options.progress = 1;
                        action.finished = true;
                    }

                    action.started = true;

                    // Call method
                    action.getMethod().Invoke(action.script, new object[] { options });
                }
            }
        }
    }

    // Reset the FX flags
    private void ResetFXs() {
        foreach (TimelineAction action in actions) {
            action.started = false;
            action.finished = false;
        }
    }
}
