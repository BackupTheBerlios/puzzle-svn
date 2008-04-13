using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;
using System.Runtime.Serialization;
using System.Data;
using GenerationStudio.Gui;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof(ColumnsElement))]
    [ElementName("Column")]
    [ElementIcon("GenerationStudio.Images.column.gif")]
    public class ColumnElement : NamedElement
    {
        [OptionalField]
        private string dbType;
        public string DbType
        {
            get
            {
                return dbType;
            }
            set
            {
                dbType = value;
                OnNotifyChange();
            }
        }



        [OptionalField]
        private bool isIdentity;
        public bool IsIdentity
        {
            get
            {
                return isIdentity;
            }
            set
            {
                isIdentity = value;
                OnNotifyChange();
            }
        }

        [OptionalField]
        private Type nativeType;
        public Type NativeType
        {
            get
            {
                return nativeType;
            }
            set
            {
                nativeType = value;
            }
        }

        [OptionalField]
        private int ordinal;
        public int Ordinal
        {
            get
            {
                return ordinal;
            }
            set
            {
                ordinal = value;
            }
        }

        [OptionalField]
        private int maxLength;
        public int MaxLength
        {
            get
            {
                return maxLength;
            }
            set
            {
                maxLength = value;
            }
        }

        [OptionalField]
        private int autoIncrementSeed;
        public int AutoIncrementSeed
        {
            get
            {
                return autoIncrementSeed;
            }
            set
            {
                autoIncrementSeed = value;
            }
        }


        [OptionalField]
        private int autoIncrementStep;
        public int AutoIncrementStep
        {
            get
            {
                return autoIncrementStep;
            }
            set
            {
                autoIncrementStep = value;
            }
        }

        [OptionalField]
        private bool isUnique;
        public bool IsUnique
        {
            get
            {
                return isUnique;
            }
            set
            {
                isUnique = value;
            }
        }

        [OptionalField]
        private bool isNullable;
        public bool IsNullable 
        {
            get
            {
                return isNullable;
            }
            set
            {
                isNullable = value;
            }
        }

        [OptionalField]
        private string defaultValue;
        public string DefaultValue
        {
            get
            {
                return defaultValue;
            }
            set
            {
                defaultValue = value;
            }
        }

        [OptionalField]
        private bool isAutoIncrement;
        public bool IsAutoIncrement
        {
            get
            {
                return isAutoIncrement;
            }
            set
            {
                isAutoIncrement = value;
            }
        }

        public override string GetIconName()
        {
            if (IsIdentity)
                return "GenerationStudio.Images.pk.gif";
            else                
                return base.GetIconName();
        }

        [ElementVerb("Toggle Identity")]
        public void ToggleIdentity(IHost host)
        {
            IsIdentity = !IsIdentity;
        }

        public override int GetSortPriority()
        {
            return this.Ordinal;
        }

        public override IList<ElementError> GetErrors()
        {
            List<ElementError> errors = new List<ElementError>();
            if (string.IsNullOrEmpty (DbType))
                errors.Add (new ElementError (this, string.Format ("Column {0}.{1} is missing DbType",Parent.GetDisplayName (),GetDisplayName ())));

            return errors;
        }
    }
}
