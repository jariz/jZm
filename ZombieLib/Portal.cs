using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieAPI
{
    /// <summary>
    /// The Portal utilty class enables you to easily teleport players to a certain location when they step into a certain area
    /// </summary>
    /// <example>
    /// Portal p = new Portal(10f, 20f, 50f);
    /// p.PortalTriggered += new PortalHandler(p_PortalTriggered);
    /// API.Portals.Add(p);
    /// </example>
    public class Portal
    {
        /// <summary>
        /// The Portal utilty class enables you to easily teleport players to a certain location when they step into a certain area
        /// </summary>
        /// <param name="X">The X coordinate that'll be used as location of the portal</param>
        /// <param name="Y">The Y coordinate that'll be used as location of the portal</param>
        /// <param name="Radius">The radius (area) the player needs to get in to be teleported</param>
        public Portal(float X, float Y, float Radius)
        {
            this.x = X;
            this.y = Y;
            this.radius = Radius;
        }

        /// <summary>
        /// The Portal utilty class enables you to easily teleport players to a certain location when they step into a certain area
        /// </summary>
        /// <param name="X">The X coordinate that'll be used as location of the portal</param>
        /// <param name="Y">The Y coordinate that'll be used as location of the portal</param>
        /// <param name="Radius">The radius (area) the player needs to get in to be teleported</param>
        /// <param name="Teleport">Will the player get teleported to <see cref="Portal.Destination"/> once triggered?</param>
        /// <param name="Destination">A Vec3 (float array with 3 values) that specifies where players will be teleported into.</param>
        public Portal(float X, float Y, float Radius, bool Teleport, float[] Destination)
        {
            this.x = X;
            this.y = Y;
            this.radius = Radius;
            this.tp = Teleport;
            this.dest = Destination;
        }


        float x;
        float y;
        float radius;
        float[] dest;
        bool tp = false;

        /// <summary>
        /// Gets triggered whenever a player steps into this portal
        /// </summary>
        public event PortalHandler PortalTriggered;

        /// <summary>
        /// The radius (area) the player needs to get in to be teleported
        /// </summary>
        public float Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }

        /// <summary>
        /// The X coordinate that'll be used as location of the portal
        /// </summary>
        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        /// <summary>
        /// The Y coordinate that'll be used as location of the portal
        /// </summary>
        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        /// <summary>
        /// Will the player get teleported to <see cref="Portal.Destination"/> once triggered?
        /// </summary>
        public bool Teleport
        {
            get
            {
                return tp;
            }
            set
            {
                tp = value;
            }
        }

        /// <summary>
        /// A Vec3 (float array with 3 values) that specifies where players will be teleported into.
        /// </summary>
        /// <remarks>
        /// Will only be used if <see cref="Portal.Teleport"/> is true
        /// </remarks>
        public float[] Destination
        {
            get
            {
                return dest;
            }
            set
            {
                dest = value;
            }
        }

        internal void trigger(Portal p, GameObjects.Player pp)
        {
            if (PortalTriggered != null)
                PortalTriggered(p, pp);
        }
    }
}
