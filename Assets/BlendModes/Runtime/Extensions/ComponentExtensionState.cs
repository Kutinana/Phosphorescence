// Copyright 2014-2024 Elringus (Artyom Sovetnikov). All Rights Reserved.

using System;
using UnityEngine;

namespace BlendModes
{
    [Serializable]
    public class ComponentExtensionState
    {
        public Component ExtendedComponent { get => extendedComponent; set => extendedComponent = value; }
        public ShaderProperty[] ShaderProperties { get => shaderProperties; set => shaderProperties = value; }

        [SerializeField] private Component extendedComponent;
        [SerializeField] private ShaderProperty[] shaderProperties = Array.Empty<ShaderProperty>();
    }
}
