using Entitas;
using UnityEngine;

public class EmitInputSystem : IInitializeSystem, IExecuteSystem
{
    readonly InputContext _context;
    private InputEntity _leftMouseEntity;

    public EmitInputSystem(Contexts contexts)
    {
        _context = contexts.input;
    }

    public void Initialize()
    {
        // initialize the unique entities that will hold the mouse button data
        _context.isLeftMouse = true;
        _leftMouseEntity = _context.leftMouseEntity;
    }

    public void Execute()
    {
        // left mouse button
        if (Input.GetMouseButtonDown(0))
            _leftMouseEntity.ReplaceMouseDown(Input.mousePosition);
        
        if (Input.GetMouseButton(0))
            _leftMouseEntity.ReplaceMousePosition(Input.mousePosition);
        
        if (Input.GetMouseButtonUp(0))
            _leftMouseEntity.ReplaceMouseUp(Input.mousePosition);

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                _leftMouseEntity.ReplaceMousePosition(Input.touches[0].position);
            }

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                _leftMouseEntity.ReplaceMouseUp(Input.touches[0].position);
            }
        }
        
        
    }
}