using UnityEngine;
using CollectionCardGame.Infrastructure;

namespace CollectionCardGame.Gameplay
{
    public class HandController : MonoBehaviour
    {
        [SerializeField] private LayerMask _usableLayer;
        [SerializeField] private Camera _camera => Camera.main;

        private Vector3 mousePoint;

        public bool dragIsAvailable = true;
        public bool useIsAvailable = true;
        public bool selectIsAvailable = true;

        private void Update()
        {
            if (_camera != null) { mousePoint = _camera.ScreenToWorldPoint(Input.mousePosition); }

            RayCheck();
        }

        
        private void RayCheck()
        {
            if (!RayDetect().Equals(default(RaycastHit)))
            {
                if ((RayDetect().collider.gameObject.TryGetComponent(out IUsable usableObj) && Input.GetMouseButtonDown(0) && !isDraging) && useIsAvailable)
                {
                    Use(usableObj);
                }

                if (RayDetect().collider.gameObject.TryGetComponent(out ISelectable selectableObj)&&!isDraging&&selectIsAvailable)
                {
                    Select(selectableObj);
                }
                else if (currentSelectObj != null)
                {
                    Deselect(currentSelectObj);
                    currentSelectObj = null;
                }

                if (RayDetect().collider.gameObject.TryGetComponent(out IDragable dragableObj) && Input.GetMouseButtonDown(0) && dragIsAvailable)
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
            
            if (Input.GetMouseButtonUp(0) && isDraging)
            { 
                Drop(currentDragableObj); 
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
            usableObj.Used?.Invoke();
        }
    }
}
