using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.Player
{
    public interface IPlayerWallChecker
    {
        bool IsCollidingWithWalls();
        bool IsCollidingWithRightWall();
        bool IsCollidingWithLeftWall();
    }


    public interface IPlayerGroundChecker
    {
        bool IsOnGround();
    }

    public class PlayerChecker : MonoBehaviour, IPlayerGroundChecker, IPlayerWallChecker
    {
        private PlayerConfig _config;
        [Inject]
        //public PlayerChecker(PlayerConfig config) {
        //    Debug.Log("asdfasdfasdf"); Debug.Log(config);
        //    _config = config;
        //}
        public void Construct(PlayerConfig config)
        {
            _config = config;
        }
        public bool IsOnGround()
        {
            return Physics.SphereCast(
                transform.position,
                _config.GroundCheckFigureSize,
                Vector3.down,
                out _,
                _config.WallCheckFigureShiftDistance,
                _config.GroundLayerMask);
        }

        public bool IsCollidingWithWalls()
        {
            // return IsCollidingWithRightWall();
            return IsCollidingWithRightWall() || IsCollidingWithLeftWall();
        }
        public bool IsCollidingWithRightWall()
        {
            if (Physics.CheckCapsule(
                transform.position,
                transform.position + transform.TransformDirection(_config.RightWallCheckDirection).normalized * _config.WallCheckFigureShiftDistance,
                _config.WallCheckFigureSize,
                _config.WallLayerMask)) return true;

            return false;
        }

        public bool IsCollidingWithLeftWall()
        {
            if (Physics.CheckCapsule(
                transform.position,
                transform.position + transform.TransformDirection(_config.LeftWallCheckDirection).normalized * _config.WallCheckFigureShiftDistance,
                _config.WallCheckFigureShiftDistance,
                _config.WallLayerMask)) return true;
            return false;
        }


        void OnDrawGizmos()
        {
            if (_config.ShowGroundCheckGizmos) Gizmos.DrawSphere(transform.position + Vector3.down * _config.GroundCheckFigureShiftDistance, _config.GroundCheckFigureSize);
            if (_config.ShowWallCheckGizmos)
            {
                //Gizmos.DrawSphere(transform.position + transform.TransformDirection(_config.LeftWallCheckDirection).normalized * _config.WallCheckFigureShiftDistance, _config.WallCheckFigureSize);
                //Gizmos.DrawSphere(transform.position + transform.TransformDirection(_config.RightWallCheckDirection).normalized * _config.WallCheckFigureShiftDistance, _config.WallCheckFigureSize);
                DrawCapsuleGizmo(
                    transform.position,
                    transform.position + transform.TransformDirection(_config.RightWallCheckDirection).normalized * _config.WallCheckFigureShiftDistance,
                    _config.WallCheckFigureSize
                );
                DrawCapsuleGizmo(
                    transform.position,
                    transform.position + transform.TransformDirection(_config.LeftWallCheckDirection).normalized * _config.WallCheckFigureShiftDistance,
                    _config.WallCheckFigureSize
                );
            }
        }

        private void DrawCapsuleGizmo(Vector3 start, Vector3 end, float radius)
        {
            // Нарисовать линии между сферами
            Vector3 up = Vector3.up * radius;
            Vector3 right = Vector3.right * radius;
            Vector3 forward = Vector3.forward * radius;

            // Соединяем окружности (псевдо-цилиндр)
            Gizmos.DrawLine(start + up, end + up);
            Gizmos.DrawLine(start - up, end - up);
            Gizmos.DrawLine(start + right, end + right);
            Gizmos.DrawLine(start - right, end - right);
            Gizmos.DrawLine(start + forward, end + forward);
            Gizmos.DrawLine(start - forward, end - forward);

            // Нарисовать сферы на концах
            Gizmos.DrawWireSphere(start, radius);
            Gizmos.DrawWireSphere(end, radius);
        }

    }
}

