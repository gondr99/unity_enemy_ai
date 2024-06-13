using System;
using UnityEngine;

namespace Gondr.Astar
{
    public class Node : IComparable<Node>
    {
        public Vector3Int pos;
        public Node _parent;
        public float G;
        public float F;

        public int CompareTo(Node other)
        {
            if (other.F == F)
                return 0;
            else
                return other.F < F ? -1 : 1;
        }

        public override bool Equals(object obj) => this.Equals(obj as Node);
        public override int GetHashCode() => pos.GetHashCode();
        public bool Equals(Node p)
        {
            if (p is null)
            {
                return false;
            }

            if (this.GetType() != p.GetType())
            {
                return false;
            }

            return pos == p.pos;
        }

        public static bool operator ==(Node lhs, Node rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }
                return false;
            }
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Node lhs, Node rhs) => !(lhs == rhs);
    }

}
