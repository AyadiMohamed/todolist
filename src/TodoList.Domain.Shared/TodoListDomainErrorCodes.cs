namespace TodoList;

public static class TodoListDomainErrorCodes
{
    /* You can add your business exception error codes here, as constants */
    #region Members
    public const string TODOLIST_MEMBER_CREATION_FAILED = "The Member creation process failed for the following reasons: ";
    public const string TODOLIST_MEMBER_ALREADY_EXIST = "A Member with the same Name exit";
    public const string TODOLIST_MEMBER_WITH_ID_NOT_FOUND = "The member with Id {0} does not exist";
    #endregion

    #region Tasks
    public const string TODOLIST_TASK_WITH_ID_NOT_FOUND = "The Task with Id {0} does not exist";
    public const string TODOLIST_TASK_CREATION_FAILED = "The task creation process failed for the following reasons: ";
    public const string TODOLIST_CANT_ASSIGN_TASKS_TO_OTHER = "Cannot Assign tasks to other members ";
    public const string TODOLIST_TASK_ALREADY_EXITS = "task with Title {0} already exits";

    #endregion


}
