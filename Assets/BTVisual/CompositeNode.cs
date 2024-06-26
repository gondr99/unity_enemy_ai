﻿using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public abstract class CompositeNode : Node
    {
        [HideInInspector] public List<Node> children = new List<Node>();
        
        public override Node Clone()
        {
            CompositeNode node = Instantiate(this);
            node.children = children.ConvertAll(c => c.Clone()); //Linq의 Mapping
            return node;
        }
    }
}