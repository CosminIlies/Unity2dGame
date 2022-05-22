
using UnityEngine;

public class Timer
{
    private float timeToWait = 0;
    private float timeWaited = 0;

    public bool isFinished = false;
    public bool isStopped = false;
    public Timer(float timeToWait)
    {
        this.timeToWait = timeToWait;
    }

    public void updateTimer()
    {

        if(!isStopped){
            if (timeToWait > timeWaited)
            {
                timeWaited += Time.deltaTime;
            
            }else
            {
                isFinished = true;
            }
        }

    }

    public void reset()
    {
        this.timeWaited = 0;
        isFinished = false;
    }

    public void set(float timeToWait)
    {
        this.timeWaited = 0;
        this.timeToWait = timeToWait;
        isFinished = false;
    }
    public float getTime()
    {
        return timeWaited;
    }
}
