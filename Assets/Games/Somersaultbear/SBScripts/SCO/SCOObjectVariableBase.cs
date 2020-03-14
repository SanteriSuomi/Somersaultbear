using UnityEngine;

namespace Somersaultbear
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "<Pending>")]
    public class SCOObjectVariableBase<T> : ScriptableObject
    {
        [SerializeField]
        private T value = default;
        public T Value
        {
            get => value;
            set => this.value = value;
        }
    }
}