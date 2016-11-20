namespace HiLaarIsch.Contract.Commands
{
    public class UpdateModelCommand<TModel>
        where TModel : class, new()
    {
        public UpdateModelCommand(TModel model, int id)
        {
            this.Model = model;
            this.Id = id;
        }

        public TModel Model { get; }
        public int Id { get; }
    }
}
