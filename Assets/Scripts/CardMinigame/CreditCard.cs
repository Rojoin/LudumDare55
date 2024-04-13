using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CreditCardMinigame
{
    public class CreditCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
        public float targetPosY = 0;

        #endregion

        #region UnityFlow

        private void Start()
        {
            initialPosition = posnetRect.position + posnetRect.localScale / 2 + offset;
            transform.position = initialPosition;
            prevPosY = transform.position.y;

            onCardDenied.AddListener(ResetCard);
        }

       

        private void OnDestroy()
        {
            onCardDenied.RemoveAllListeners();
            onCardApproved.RemoveAllListeners();
        }

        #endregion

        #region CustomMethods

  
        private void ResetCard()
        {
            resetingCoroutine = StartCoroutine(ResetCardCoroutine());
            timeHeld = 0;
        }

        private void CheckCard()
        {
            if (movingCoroutine != null)
            {
                StopCoroutine(movingCoroutine);
                movingCoroutine = null;
            }

            if (transform.position.y <= targetPosY && timeHeld < maxTime && timeHeld > minTime)
            {
                onCardApproved.Invoke();
                Debug.Log("Approve");
            }
            else
            {
                Debug.Log("Denied");
                onCardDenied.Invoke();
            }
        }


        #endregion

        #region Coroutines

        IEnumerator ResetCardCoroutine()
        {
            //float duration = Vector3.Distance(transform.position, initialPosition) / cardResetSpeed;
            isResetCoroutineRunning = true;
            float timer = 0;
            float prevTime = Time.time;
            while (timer < cardResetSpeed)
            {
                float actualTime = Time.time;
                timer += actualTime - prevTime;
                prevTime = actualTime;
                transform.position = Vector3.Lerp(transform.position, initialPosition, timer / cardResetSpeed);
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
            targetPosY = posnetRect.position.y - posnetRect.localScale.y / 2;
            while (transform.position.y > targetPosY && isHeld)
            {
                timeHeld += Time.deltaTime;

                float posY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
                posY = Mathf.Clamp(posY, targetPosY, initialPosY);
                transform.position = new Vector3(transform.position.x, posY);
                yield return null;
            }

            Debug.Log("Termino la corrutina");
            CheckCard();
        }

        #endregion


        public void OnPointerDown(PointerEventData eventData)
        {
            isHeld = true;
            if (isResetCoroutineRunning)
            {
                StopCoroutine(resetingCoroutine);
                transform.position = initialPosition;
                prevPosY = transform.position.y;
                isResetCoroutineRunning = false;
            }
            if (movingCoroutine == null)
            {
                movingCoroutine = StartCoroutine(MoveCoroutine());
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isHeld = false;
            if (movingCoroutine != null)
            {
                StopCoroutine(movingCoroutine);
                CheckCard();
            }
        }
        
    }
}