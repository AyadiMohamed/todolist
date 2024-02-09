namespace TodoList.Permissions;

public static class TodoListPermissions
{
    public const string GroupName = "TodoList";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class TodoListTasks
    {
        public const string Default = GroupName + ".Tasks";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string Read = Default + ".Read";
    }

    public static class TodoListMembers
    {
        public const string Default = GroupName + ".Members";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string Read = Default + ".Read";
    }
}
