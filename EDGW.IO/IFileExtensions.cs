namespace EDGW.IO
{
    public static class IFileExtensions
    {
        public static bool IsModifiable(this IFile file) => file.UsingType.IsModifiable;
        public static bool IsRemovable(this IFile file) => file.UsingType.IsRemovable;
        public static bool IsReadable(this IFile file) => file.UsingType.IsReadable;
    }
}