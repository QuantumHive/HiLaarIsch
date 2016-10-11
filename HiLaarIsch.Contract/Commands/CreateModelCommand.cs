namespace HiLaarIsch.Contract.Commands
{
    public class CreateModelCommand<TModel>
        where TModel : class, new()
    {
        public CreateModelCommand(TModel model)
        {
            this.Model = model;
        }

        public TModel Model { get; }
    }
}
