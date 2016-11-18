using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.ClassAndGeneric
{
    public partial class GeneratedEntity
    {
        public GeneratedEntity(string entityName)
        {
            this.EntityName = entityName;
        }

        partial void ChangingProperty(string name, string originName, string newName);

        public string EntityName { get; }
        private string _FirstName;

        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                ChangingProperty("FirstName", _FirstName, value);
                _FirstName = value;
            }
        }

        private string _State;

        public string State
        {
            get { return _State; }
            set
            {
                ChangingProperty("State", _State, value);
                _FirstName = value;
            }
        }
    }

    public partial class GeneratedEntity
    {
        partial void ChangingProperty(string name, string originName, string newName)
        {
            Console.WriteLine($"Changed property ({name}) for entity {this.EntityName} from {originName} to {newName}");
        }
    }
}
