using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CreditCard
{
    public class CreditCard : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Transform posnetRect;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float minTime;
        [SerializeField] private float maxTime;
        [SerializeField] private float cardResetSpeed;
        private float timeHeld = 0;
        private float prevPosY;
        private bool wasCardAccepted;
        private bool isHeld = false;
        private Vector3 initialPosition;
        private bool isResetCoroutineRunning = false;
        private Coroutine movingCoroutine;
        private Coroutine resetingCoroutine;
        public UnityEvent onCardApproved;
        public UnityEvent onCardDenied;
        #endregion

        #region UnityFlow
        private void Start()
        {
            initialPosition = posnetRect.position + posnetRect.localScale / 2 + offset;
            transform.position = initialPosition;
            prevPosY = transform.position.y;

            onCardDenied.AddListener(ResetCard);
        }
        void Update()
        {
            isHeld = Input.GetMouseButton(0);

            if (isHeld && movingCoroutine == null && !isResetCoroutineRunning)
            {
                Debug.Log("APRETADO");
                movingCoroutine = StartCoroutine(MoveCoroutine());
            }
        }
        private void OnDestroy()
        {
            onCardDenied.RemoveAllListeners();
            onCardApproved.RemoveAllListeners();
        }
        #endregion

        #region CustomMethods
        //private void Move()
        //{
        //    timeHeld += Time.deltaTime;

        //    float posY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        //    float minPosY = posnetRect.position.y - posnetRect.localScale.y / 2;
        //    float maxPosY = posnetRect.position.y + posnetRect.localScale.y / 2;
        //    posY = Mathf.Clamp(posY, minPosY, maxPosY);

        //    if (posY > prevPosY)
        //    {
        //        Debug.Log($"{posY} is bigger than {prevPosY}, mouse went up");
        //        Input.ResetInputAxes();
        //        return;
        //    }

        //    transform.position = new Vector3(transform.position.x, posY);
        //    prevPosY = transform.position.y;

        //    if (transform.position.y == minPosY)
        //        CheckCard();
        //}
        private void ResetCard()
        {
            if (!isResetCoroutineRunning)
            {
                if (resetingCoroutine != null)
                    StopCoroutine(resetingCoroutine);
                resetingCoroutine = StartCoroutine(ResetCardCoroutine(cardResetSpeed));
            }
            timeHeld = 0;
        }
        private void CheckCard(float targetPosY)
        {
            if (transform.position.y <= targetPosY && timeHeld < maxTime && timeHeld > minTime)
            {
                wasCardAccepted = true;
                onCardApproved.Invoke();
            }
            else
            {
                onCardDenied.Invoke();
            }
        }
        #endregion

        #region Coroutines
        IEnumerator ResetCardCoroutine(float duration)
        {
            if (movingCoroutine != null)
            {
                StopCoroutine(movingCoroutine);
                Debug.Log(movingCoroutine);
            }
            //float duration = Vector3.Distance(transform.position, initialPosition) / cardResetSpeed;
            isResetCoroutineRunning = true;
            float timer = 0;
            float prevTime = Time.time;
            while (timer < duration)
            {
                float actualTime = Time.time;
                timer += actualTime - prevTime;
                prevTime = actualTime;
                transform.position = Vector3.Lerp(transform.position, initialPosition, timer / duration);
                yield return null;
            }
            transform.position = initialPosition;
            prevPosY = transform.position.y;
            isResetCoroutineRunning = false;
        }

        private IEnumerator MoveCoroutine()
        {
            timeHeld = 0;
            float initialPosY = posnetRect.position.y + posnetRect.localScale.y / 2;
            float targetPosY = posnetRect.position.y - posnetRect.localScale.y / 2;
            while (transform.position.y < targetPosY || isHeld)
            {
                timeHeld += Time.deltaTime;

                float posY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
                posY = Mathf.Clamp(posY, targetPosY, initialPosY);

                if (posY > prevPosY)
                {
                    Debug.Log($"{posY} is bigger than {prevPosY}, mouse went up");
                    Input.ResetInputAxes();
                    yield break;
                }

                transform.position = new Vector3(transform.position.x, posY);
                prevPosY = transform.position.y;
                yield return null;
            }
            CheckCard(targetPosY);
        }
        #endregion
    }
}