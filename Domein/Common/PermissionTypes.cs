namespace Domein.Common
{
    public enum PermissionTypes
    {
        #region AnswerPermissions
        GetAnswerById,
        GetAllAnswer,
        CreateAnswer,
        UpdateAnswer,
        DeleteAnswer,
        #endregion

        #region UserPermissions
        GetUserById,
        GetAllUser,
        CreateUser,
        UpdateUser,
        DeleteUser,
        #endregion

        #region ClientPermissions
        GetClientById,
        GetAllClient,
        CreateClient,
        UpdateClient,
        DeleteClient,
        #endregion

        #region QuestionPermissions
        GetQuestionById,
        GetAllQuestion,
        CreateQuestion,
        UpdateQuestion,
        DeleteQuestion,
        #endregion

        #region DistrictPermissions
        GetDistrictById,
        GetAllDistrict,
        CreateDistrict,
        UpdateDistrict,
        DeleteDistrict,
        #endregion

        #region RegionPermissions
        GetRegionById,
        GetAllRegion,
        CreateRegion,
        UpdateRegion,
        DeleteRegion,
        #endregion

        #region RolePermissions
        GetRoleById,
        GetAllRole,
        CreateRole,
        UpdateRole,
        DeleteRole,
        #endregion

        #region PermissionPermissions
        GetPermissionById,
        GetAllPermission,
        CreatePermission,
        UpdatePermission,
        DeletePermission,
        #endregion


        #region CategoryPermissions
        GetCategoryById,
        GetAllCategory,
        CreateCategory,
        UpdateCategory,
        DeleteCategory,
        #endregion


        #region QuarterPermissions
        GetQuarterById,
        GetAllQuarter,
        CreateQuarter,
        UpdateQuarter,
        DeleteQuarter,
        #endregion

    }
}
