using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceRoller : MonoBehaviour
{
    public Action<int> OnDieRolled;

    [SerializeField] private float _rollForce = 5f;
    [SerializeField] private float _torqueAmount = 10f;
    
    private Rigidbody _rigidbody;
    private Transform _transform;
    private bool _rolling;

    private DiceSides _diceSides;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _diceSides = GetComponent<DiceSides>();
    }

    private void Start()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        if (!_rolling)
        {
            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
                Input.GetMouseButtonDown(0))
            {
                RollDie();
            }
        }
    }

    [ContextMenu("Roll Die")]
    public void RollDie()
    {
        StopAllCoroutines();
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rolling = true;
        var targetPosition = new Vector3(Random.Range(-1f, 1f), 10f, Random.Range(-1f, 1f));
        var direction = targetPosition - _transform.position;
        _rigidbody.AddForce(direction * _rollForce, ForceMode.Impulse);
        _rigidbody.AddTorque(Random.insideUnitSphere * _torqueAmount, ForceMode.Impulse);
        StartCoroutine(WaitForDieToStop());
    }

    private IEnumerator WaitForDieToStop()
    {
        var timeOut = Time.time + 5f;
        while (!_rigidbody.IsSleeping() && Time.time < timeOut)
        {
            yield return null;
        }

        yield return null;

        var dieValue = _diceSides.GetMatch();
        Debug.Log($"You rolled a {dieValue}!", gameObject);
        _rolling = false;
        OnDieRolled?.Invoke(dieValue);
    }
}
