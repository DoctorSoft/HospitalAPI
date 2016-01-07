using StorageModels.Interfaces;

namespace HandleToolsInterfaces.RepositoryHandlers
{
    public interface ITwoSideShowingHandler<T>
        where T: IIdModel, IShowStatusModel
    {
        T HideModeFromFromSide(T model);

        T HideModelFromToSide(T model);

        T HideModel(T model);

        T ShowModelForFromSide(T model);

        T ShowModelForToSide(T model);

        T ShowModel(T model);
    }
}
