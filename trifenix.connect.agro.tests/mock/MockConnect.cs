using Cosmonaut;
using Moq;
using System.Linq;
using trifenix.connect.agro.external.helper;
using trifenix.connect.agro.interfaces.cosmos;
using trifenix.connect.entities.cosmos;
using trifenix.connect.input;
using trifenix.connect.interfaces.db.cosmos;
using trifenix.connect.interfaces.external;
using trifenix.connect.interfaces.graph;

namespace trifenix.connect.agro.tests.mock
{
    public class MockConnect : IDbAgroConnect
    {

        // no se usa en la mayor�a de las operaciones
        public ICosmosStore<EntityContainer> BatchStore
        {
            get
            {

                return null;
            }
        }


        public ICommonQueries CommonQueries
        {
            get
            {
                var mock = new Mock<ICommonQueries>();
                // definici�n de m�todos.



                return mock.Object;
            }
        }

        public IGraphApi GraphApi
        {
            get
            {
                var mock = new Mock<IGraphApi>();
                // definici�n de m�todos.



                return mock.Object;
            }
        }

        public IDbExistsElements GetDbExistsElements => MockHelper.GetExistElement();

        public IExistElement ExistsElements(bool isBatch)
        {
            var mock = new Mock<IExistElement>();
            // definici�n de m�todos.
            return mock.Object;
        }


        public ICommonDbOperations<T> GetCommonDbOp<T>() where T : DocumentBase
        {
            var mock = new Mock<ICommonDbOperations<T>>();
            // definici�n de m�todos.


            return mock.Object;
        }

        public IMainGenericDb<T> GetMainDb<T>() where T : DocumentBase
        {
            var mock = new Mock<IMainGenericDb<T>>();
            // definici�n de m�todos.


            return mock.Object;
        }

        public IValidatorAttributes<T_INPUT, T_DB> GetValidator<T_INPUT, T_DB>(bool isBatch)
            where T_INPUT : InputBase
            where T_DB : DocumentBase
        {
            return new MainValidator<T_DB, T_INPUT>(MockHelper.GetExistElement());
        }
    }
}
