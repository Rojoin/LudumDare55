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

        [SerializeField] private RectTransform cardTransform;
        [SerializeField] private float minTime;
        [SerializeField] private float maxTime;
        [SerializeField] private float cardResetSpeed;
        [SerializeField] RectTransform initPosition;
        [SerializeField] RectTransform endPosition;
        private float timeHeld = 0;
        private bool wasCardAccepted;
        private bool isHeld = false;
        private Vector2 initialPosition;
        private Vector2 endingPosition;
        private bool isResetCoroutineRunning = false;
        private Coroutine movingCoroutine;
        private Coroutine resetingCoroutine;
        public UnityEvent onCardApproved;
        public UnityEvent onCardDenied;
        public float targetPosY = 0;
        [SerializeField] private float offsetY  =20;

        #endregion

        #region UnityFlow

        private void OnEnable()
        {
            initialPosition = new Vector2(initPosition.anchoredPosition.x,initPosition.anchoredPosition.y);
            endingPosition =  new Vector2(endPosition.anchoredPosition.x,endPosition.anchoredPosition.y);
            targetPosY = endingPosition.y;
            cardTransform.anchoredPosition = initialPosition;
            onCardDenied.AddListener(ResetCard);
        }
        
        private void OnDisable()
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

            if (cardTransform.anchoredPosition.y <= targetPosY && timeHeld < maxTime && timeHeld > minTime)
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
                cardTransform.anchoredPosition = Vector2.Lerp(cardTransform.anchoredPosition, initialPosition, timer / cardResetSpeed);
                yield return null;
            }
            cardTransform.anchoredPosition = initialPosition;
            isResetCoroutineRunning = false;
        }

        private IEnumerator MoveCoroutine()
        {
            timeHeld = 0;
          
            while (cardTransform.anchoredPosition .y > targetPosY && isHeld)
            {
                timeHeld += Time.deltaTime;

                Vector2 mousePosRelativeToCenter = (Vector2)Input.mousePosition;
                float posY = mousePosRelativeToCenter.y + offsetY;
                posY = Mathf.Clamp(posY, endingPosition.y, initialPosition.y);
                cardTransform.anchoredPosition = new Vector2(cardTransform.anchoredPosition.x, posY);
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
                cardTransform.anchoredPosition = initialPosition;
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