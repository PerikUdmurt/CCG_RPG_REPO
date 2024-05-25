using System.Collections;
using UnityEngine;

namespace CollectionCardGame.Gameplay
{
    public class HandController : MonoBehaviour
    {
        [SerializeField] private float _useTriggerTime;
        [SerializeField] private LayerMask _usableLayer;
        [SerializeField] private Camera _camera => Camera.main;

        private Vector3 mousePoint;

        public bool dragIsAvailable = true;
        public bool useIsAvailable = true;
        public bool selectIsAvailable = true;
        private bool _useTrigger = false;

        private void Update()
        {
            if (_camera != null) { mousePoint = _camera.ScreenToWorldPoint(Input.mousePosition); }

            RayCheck();
        }

        
        private void RayCheck()
        {

            if (!RayDetect().Equals(default(RaycastHit)))
            {
                if (RayDetect().collider.gameObject.TryGetComponent(out IUsable usableObj) && Input.GetMouseButtonUp(0) && useIsAvailable && usableObj.isUsable)
                {
                    Use(usableObj);
                }

                if (RayDetect().collider.gameObject.TryGetComponent(out ISelectable selectableObj)&&selectIsAvailable&& selectableObj.isSelectable)
                {
                    Select(selectableObj);
                }
                else if (currentSelectObj != null)
                {
                    Deselect(currentSelectObj);
                    currentSelectObj = null;
                }

                if (RayDetect().collider.gameObject.TryGetComponent(out IDragable dragableObj) && Input.GetMouseButtonDown(0) && dragIsAvailable&& dragableObj.isDragable)
                {
                    currentDragableObj = dragableObj;
                    Drag(dragableObj);
                    dragableObj.Taken?.Invoke();
                }
            }
            else 
            { 
                Deselect(currentSelectObj);
            }

            if (Input.GetMouseButton(0) && isDraging && dragIsAvailable)
            {
                Drag(currentDragableObj);
            }
            
            if (Input.GetMouseButtonUp(0))
            { 
                if (currentDragableObj != null) { Drop(currentDragableObj); }
            }
        }

        private RaycastHit RayDetect()
        {
            
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _usableLayer)) return hitInfo;
            else return default(RaycastHit);
        }

        [SerializeField] private float dragLerpValue;
        private bool isDraging = false;
        private IDragable currentDragableObj = null;
        private void Drag(IDragable dragableObj)
        {
            isDraging = true;
            dragableObj.tf.transform.position = Vector3.Lerp(dragableObj.tf.transform.position, new Vector3(mousePoint.x, mousePoint.y, 0), dragLerpValue);
            Deselect(currentSelectObj);
        }

        private void Drop(IDragable dragableObj)
        {
            currentDragableObj.Dropped?.Invoke();
            currentDragableObj = null;
            isDraging = false;
        }

        private ISelectable currentSelectObj = null;
        private void Select(ISelectable selectableObj)
        {
            if (currentSelectObj != selectableObj)
            {
                Deselect(currentSelectObj);
                currentSelectObj = selectableObj;
                selectableObj.Selected?.Invoke();
            }
        }

        private void Deselect(ISelectable selectableObj)
        {
            if (currentSelectObj == null) return;
            selectableObj.Deselected?.Invoke();
            currentSelectObj = null;
        }

        private void Use(IUsable usableObj)
        {
            usableObj.Use();
        }

        private IEnumerator TimerForUse(float time)
        {
            _useTrigger = true;
            yield return new WaitForSeconds(time);
            _useTrigger = false;
        }
    }
}
