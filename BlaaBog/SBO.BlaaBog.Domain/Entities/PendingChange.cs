using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Domain.Entities
{
    public class PendingChange
    {
        private int? _id;
        public int? Id { get { return _id; } }

        private string? _name;
        public string? Name { get { return _name; } }

        private string? _image;
        public string Image { get { return _image; } }

        private string? _description;
        public string? Description { get { return _description; } }

        public PendingChange(int? id, string? name, string? image, string? description)
        {
            _id = id;
            _name = name;
            _image = image;
            _description = description;
        }
    }
}
