using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

    public class Proto_LaserOrigin : MonoBehaviour
    {
        [ShowAssetPreview]
        public GameObject projectileSphere;

        public enum DIRECTION
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,
            DIAGONAL
        }

        [InfoBox("Use this to set the direction that the origin will fire the laser in.", EInfoBoxType.Normal)]
        public DIRECTION dir;

        private Vector3 laserStartDir = Vector3.zero;

        // Start is called before the first frame update
        private void Start()
        {

            switch (dir)
            {
                case DIRECTION.UP:
                    transform.rotation = Quaternion.AngleAxis(0.0f, Vector3.forward);
                    laserStartDir = Vector3.up;
                    break;

                case DIRECTION.DOWN:
                    transform.rotation = Quaternion.AngleAxis(180.0f, Vector3.forward);
                    laserStartDir = Vector3.down;
                    break;

                case DIRECTION.LEFT:
                    transform.rotation = Quaternion.AngleAxis(-90.0f, Vector3.forward);
                    laserStartDir = Vector3.left;
                    break;

                case DIRECTION.RIGHT:
                    transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
                    laserStartDir = Vector3.right;
                    break;

                case DIRECTION.DIAGONAL:
                    transform.rotation = Quaternion.AngleAxis(-45.0f, Vector3.forward);
                    laserStartDir = transform.rotation * Vector3.up;
                    break;

            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                shootLaser();
            }
        }

        void OnMouseUp()
        {
            if (GameManager.gameManagerInstance.beginCountDown == false)
            {
                shootLaser();
            }
        }


        #region Naughty Attributes Test Functions

        [Button("Shoot Laser")]
        public void shootLaser()
        {
            GameManager.gameManagerInstance.Reset();
            GameManager.gameManagerInstance.beginCountDown = true;
            GameObject projectile = Instantiate(projectileSphere, transform.position, Quaternion.identity);
            projectile.GetComponent<Proto_Projectile>().DirectionVector = laserStartDir;
        }

        [Button("Change Direction of Origin Laser")]
        private void changeLaserDirection()
        {
            switch (dir)
            {
                case DIRECTION.UP:
                    laserStartDir = Vector2.right;
                    dir = DIRECTION.RIGHT;
                    break;

                case DIRECTION.RIGHT:
                    laserStartDir = Vector2.down;
                    dir = DIRECTION.DOWN;
                    break;

                case DIRECTION.DOWN:
                    laserStartDir = Vector2.left;
                    dir = DIRECTION.LEFT;
                    break;

                case DIRECTION.LEFT:
                    laserStartDir = Vector2.up;
                    dir = DIRECTION.UP;
                    break;

                default:
                    break;
            }

        }

        #endregion
    }
