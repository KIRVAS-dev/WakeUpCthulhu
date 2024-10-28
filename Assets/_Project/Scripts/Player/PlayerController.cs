using UnityEngine;


namespace CthulhuGame
{
    public enum InputType
    {
        Idle,
        Forward,
        ForwardLeft,
        ForwardRight,
        Backwards,
        BackwardsLeft,
        BackwardsRight,
        Left,
        Right,
    }

    public class PlayerController : Singleton<PlayerController>
    {
        [Header("Settings")]
        [SerializeField] private float maxThrust;
        [SerializeField] private float maxLinearVelocity;
        [SerializeField] private float maxTorque;
        [SerializeField] private float maxAngularVelocity;
        [SerializeField] private Ship playerShip;

        private InputType currentInput;
        public InputType CurrentInput => currentInput;

        private float m_Thrust = 0f;
        private float m_Torque = 0f;

        private bool canControl = true;

        private void Update()
        {
            if (!canControl)
                return;

            var movement = UI_InputController.Instance.Value;
            m_Thrust = movement.y;

            if (m_Thrust > 0f)
                currentInput = InputType.Forward;
            if (m_Thrust < 0f)
                currentInput = InputType.Backwards;
            if (m_Thrust == 0f)
                currentInput = InputType.Idle;

            /*if (Input.GetKey(KeyCode.W))
            {
                m_Thrust = 1f;
                currentInput = InputType.Forward;
            }

            if (Input.GetKey(KeyCode.S))
            {
                m_Thrust = -1f;
                currentInput = InputType.Backwards;
            }


            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                m_Thrust = 0f;
                currentInput = InputType.Idle;
            }*/

            if (Input.GetKey(KeyCode.D))
                HandleRightButton();

            if (Input.GetKey(KeyCode.A))
                HadleLeftButton();

            if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
                m_Torque = 0f;

            //Debug.Log("Thrust = " + m_Thrust + "; Torque = " + m_Torque);
        }

        private void FixedUpdate()
        {
            Move();
            Turn();
        }

        private void HadleLeftButton()
        {
            if (Mathf.Sign(m_Thrust) == Mathf.Sign(-1f))
                m_Torque = -1f;
            else
                m_Torque = 1f;

            switch (currentInput)
            {
                case InputType.Idle:
                    currentInput = InputType.Left;
                    break;

                case InputType.Forward:
                    currentInput = InputType.ForwardLeft;
                    break;

                case InputType.Backwards:
                    currentInput = InputType.BackwardsLeft;
                    break;
            }
        }

        private void HandleRightButton()
        {
            if (Mathf.Sign(m_Thrust) == Mathf.Sign(-1f))
                m_Torque = 1f;
            else
                m_Torque = -1f;

            switch (currentInput)
            {
                case InputType.Idle:
                    currentInput = InputType.Right;
                    break;

                case InputType.Forward:
                    currentInput = InputType.ForwardRight;
                    break;

                case InputType.Backwards:
                    currentInput = InputType.BackwardsRight;
                    break;
            }
        }

        private void Move()
        {
            if (m_Thrust != 0f)
                playerShip.Rigidbody.AddForce(playerShip.transform.up * m_Thrust * maxThrust * Time.fixedDeltaTime, ForceMode2D.Force);
            else
                playerShip.Rigidbody.AddForce(-playerShip.Rigidbody.linearVelocity * maxThrust / 2f * Time.fixedDeltaTime, ForceMode2D.Force);


            if (playerShip.Rigidbody.linearVelocity.magnitude >= maxLinearVelocity)
                playerShip.Rigidbody.linearVelocity = Vector2.ClampMagnitude(playerShip.Rigidbody.linearVelocity, maxLinearVelocity);
        }
        private void Turn()
        {
            if (m_Torque != 0f)
            {
                playerShip.Rigidbody.AddForce(playerShip.transform.up * maxThrust / 2f * Time.fixedDeltaTime, ForceMode2D.Force);
                playerShip.Rigidbody.AddTorque(m_Torque * maxTorque * Time.fixedDeltaTime, ForceMode2D.Force);
            }
            else
                playerShip.Rigidbody.AddTorque(-playerShip.Rigidbody.angularVelocity * maxTorque / 2f * Time.fixedDeltaTime, ForceMode2D.Force);

            if (Mathf.Abs(playerShip.Rigidbody.angularVelocity) >= maxAngularVelocity)
                playerShip.Rigidbody.angularVelocity = maxAngularVelocity * Mathf.Sign(playerShip.Rigidbody.angularVelocity);
        }

        public void SetMaxLinearVelocity(float value)
        {
            maxLinearVelocity = value;
        }

        public void ProhibitMovement()
        {
            playerShip.Rigidbody.linearVelocity = Vector2.zero;
            playerShip.Rigidbody.freezeRotation = true;
            playerShip.Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        public void AllowMovement()
        {
            playerShip.Rigidbody.freezeRotation = false;
            playerShip.Rigidbody.constraints = RigidbodyConstraints2D.None;
        }

        public void EnableControl()
        {
            canControl = true;
        }

        public void DisableControl()
        {
            canControl = false;
            Stop();
        }

        public void Stop()
        {
            //playerShip.Rigidbody.velocity = Vector3.zero;
            m_Thrust = 0f;
            m_Torque = 0f;
        }
    }
}