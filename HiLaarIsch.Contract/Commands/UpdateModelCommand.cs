using System;

namespace HiLaarIsch.Contract.Commands
{
    public class UpdateModelCommand<TModel>
        where TModel : class, new()
    {
        public UpdateModelCommand(TModel model, Guid id)
        {
            this.Model = model;
            this.Id = id;
        }

        public TModel Model { get; }
        public Guid Id { get; }

    }
}
