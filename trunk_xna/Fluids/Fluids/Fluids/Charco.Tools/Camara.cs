using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;


namespace Charco.Tools
{
   
  
    //----------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------
    public class Camara : ICamara
    {
       
        Matrix view;								// Keep matrixs aligned at the beginning
        Matrix projection;
        Matrix view_projection;
        Matrix view_projection_inverse;
        Matrix view_inverse;

        protected Vector3 position;
        protected Vector3 target;

        Vector3 front;
        Vector3 left;
        Vector3 up;
        protected Vector3 up_axis;

        float distance_to_target = 0.0f;
        float fov = MathHelper.ToRadians(120)/4;
        float aspect_ratio = 800.0f/600.0f;
        float z_min = 0.2f;
        float z_max = 4000.0f;

        public float NearPlane { get { return z_min; } }
        public float FarPlane { get { return z_max; } }

        
        public bool Debug { get; set; }

        public BoundingFrustum boundingFrustum;

        public Camara() 
        {
           
            this.Debug = false;

            up_axis.X = 0.0f;
            up_axis.Y = 1.0f;
            up_axis.Z = 0.0f;

            position.X = position.Y = -50;

            target.X = target.Y = 0.0f;

            position.Z = -50;
            target.Z = 50;

            Fov = fov;

            aspect_ratio = 800f / 600f;
            
            lookAt( position, target );            
        }

        public Matrix View { get { return view; } set { view = value; updateMatrix(); } }
        public Matrix viewMatrixInverse { get { return view_inverse; } }
        public Matrix Projection { get { return projection; } set { projection = value; updateMatrix(); } }
        public Matrix ViewProjection { get { return view_projection; } }
        public Matrix viewProjectionInverseMatrix { get { return view_projection_inverse; } }
        
        public Vector3 Target { get { return target; } }
        public Vector3 Front { get { return front; } }
        public Vector3 Left { get { return left; } }
        public Vector3 Up { get { return up; } }

        public float DistanceToTarget { get { return distance_to_target; } }
        
        public Vector3 Position { get { return position; }  }

        public BoundingFrustum Frustum { get { return boundingFrustum; } }

        public float Fov
        {
            get { return fov; }
            set
            {
                fov = value;
                projection = Matrix.CreatePerspectiveFieldOfView(fov, aspect_ratio, z_min, z_max);
                updateMatrix();
            }

        }

        
        public float AspectRatio
        {
            get { return aspect_ratio; }
            set
            {
                aspect_ratio = value;
                projection = Matrix.CreatePerspectiveFieldOfView(fov, aspect_ratio, z_min, z_max);
                updateMatrix();
            }

        }

        public void lookAt(Vector3 src, Vector3 dst)
        {
            lookAt(src, dst, up_axis);
        }

        public void lookAt( Vector3 src, Vector3 dst, Vector3 upAxis ) 
        {
            up_axis = upAxis;

            position = src;
            target   = dst;

            front = target - position;

            // Keep distance to target and normalize front
            distance_to_target = front.Length();
            float inv_d = 1.0f / distance_to_target;
            front.X *= inv_d; front.Y *= inv_d; front.Z *= inv_d;

            left = Vector3.Cross(up_axis, front);
            left.Normalize();
            up = Vector3.Cross(front, left);

            view = Matrix.CreateLookAt(position, target, up_axis);
            updateMatrix();
        }

        public void setZPlanes(float azmin, float azmax)
        {
            z_min = azmin;
            z_max = azmax;
            projection = Matrix.CreatePerspectiveFieldOfView(fov, aspect_ratio, z_min, z_max);
            updateMatrix();
        }


        private void updateMatrix()
        {
            view_projection = view * projection;
            view_inverse = Matrix.Invert(view);
            view_projection_inverse = Matrix.Invert(view_projection);

            boundingFrustum = new BoundingFrustum(view_projection);
        } 
    }
}