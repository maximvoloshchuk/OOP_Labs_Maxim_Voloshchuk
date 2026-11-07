using System;

namespace Lab10.Library
{
    // интерфейс для заполнения объектов (руками или рандомом)
    public interface IInit
    {
        void Init();
        void RandomInit();
    }
}