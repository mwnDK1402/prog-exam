namespace YouWillExplode
{
    using System;

    internal interface IManageable
    {
        event Action Terminated;

        void Initialize(Scene scene);

        void Terminate();
    }
}