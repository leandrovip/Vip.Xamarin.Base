namespace Vip.Xamarin.Base
{
    public abstract class TaskCommandResult : CommandResult
    {
        #region Propriedades

        public TaskResult TaskResult { get; set; }

        #endregion

        #region Construtores

        protected TaskCommandResult()
        {
            TaskResult = TaskResult.Success;
        }

        #endregion
    }
}