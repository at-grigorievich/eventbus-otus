using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

public class TurnPipelineRunner: MonoBehaviour
{
    [ShowInInspector]
    private TurnPipeline _turnPipeline;
    
    [Inject]
    public void Construct(TurnPipeline pipeline)
    {
        _turnPipeline = pipeline;
        _turnPipeline.AddTask(new StartTurnTask());
        _turnPipeline.AddTask(new FinishTurnTask());
    }

    [Button]
    public void RunPipeline()
    {
        _turnPipeline.Reset();
        _turnPipeline.Run();
    }
}