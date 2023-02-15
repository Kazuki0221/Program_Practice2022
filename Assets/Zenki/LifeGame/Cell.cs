using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]LifeState state;
    public void ChangeState(LifeState _state)
    {
        state = _state;
        switch (state)
        {
            case LifeState.Survive:
                gameObject.GetComponent<Renderer>().material.color = Color.green;
                break;

            case LifeState.Death:
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                break;
            default:
                break;
        }
    }
}

public enum LifeState
{
   Survive,
   Death
}
