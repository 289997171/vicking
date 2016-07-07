using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {

        // 优化性能
        //		public enum AxisOption
        //		{
        //			// Options for which axes to use
        //			Both, // Use both
        //			OnlyHorizontal, // Only horizontal
        //			OnlyVertical // Only vertical
        //		}

        // 摇杆的范围
        public int MovementRange = 100;
        //		public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
        public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
        public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

        Vector3 m_StartPos;
        //		bool m_UseX; // Toggle for using the x axis
        //		bool m_UseY; // Toggle for using the Y axis
        CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
        CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input

        void OnEnable()
        {
            CreateVirtualAxes();
        }

        void Start()
        {
            m_StartPos = transform.position;
        }

        private Vector3 delta = Vector3.zero;

        void UpdateVirtualAxes(Vector3 value)
        {
            delta = m_StartPos - value;
            delta.y = -delta.y;
            delta /= MovementRange;
            //			if (m_UseX)
            //			{
            m_HorizontalVirtualAxis.Update(-delta.x);
            //			}

            //			if (m_UseY)
            //			{
            m_VerticalVirtualAxis.Update(delta.y);
            //			}
        }

        void CreateVirtualAxes()
        {
            // set axes to use
            //			m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
            //			m_UseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

            // create new axes based on axes to use
            //			if (m_UseX)
            //			{
            m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
            CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
            //			}
            //			if (m_UseY)
            //			{
            m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
            CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
            //			}
        }

        private int deltaV3 = 0;

        private Vector3 newPos = Vector3.zero;

        public void OnDrag(PointerEventData data)
        {
            //			if (m_UseX)
            //			{
            deltaV3 = (int)(data.position.x - m_StartPos.x);
            //不支持矩形(使用圆形)
            //deltaV3 = Mathf.Clamp(deltaV3, -MovementRange, MovementRange);
            newPos.x = deltaV3;
            //			}

            //			if (m_UseY)
            //			{
            deltaV3 = (int)(data.position.y - m_StartPos.y);
            //不支持矩形(使用圆形)
            //deltaV3 = Mathf.Clamp(deltaV3, -MovementRange, MovementRange);
            newPos.y = deltaV3;
            //			}
            
            // transform.position = new Vector3(m_StartPos.x + newPos.x, m_StartPos.y + newPos.y, m_StartPos.z + newPos.z);
            transform.position = Vector3.ClampMagnitude(new Vector3(newPos.x, newPos.y, newPos.z), MovementRange) + m_StartPos;

            UpdateVirtualAxes(transform.position);
        }


        public void OnPointerUp(PointerEventData data)
        {
            transform.position = m_StartPos;
            UpdateVirtualAxes(m_StartPos);
        }


        public void OnPointerDown(PointerEventData data) { }

        void OnDisable()
        {
            // remove the joysticks from the cross platform input
            //			if (m_UseX)
            //			{
            m_HorizontalVirtualAxis.Remove();
            //			}
            //			if (m_UseY)
            //			{
            m_VerticalVirtualAxis.Remove();
            //			}
        }
    }
}