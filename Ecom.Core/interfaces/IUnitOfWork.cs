using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.interfaces
{
    public interface IUnitOfWork
    {

        public ICategoryRepositry categoryRepositry { get;  }
        public IPhotoRepositry photoRepositry{ get; }
        public IProductRepositry productRepositry{ get; }
    }
}
