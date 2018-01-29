namespace DatabaseContract
{
    using System.Collections.Generic;

    public interface IProfileDatabase
    {
        ICollection<Profile> Load();

        void Save(ICollection<Profile> profiles);
    }
}