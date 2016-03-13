using Lexemes;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Translator.Lexemes
{
    public class LexemeTable<T> : IEnumerable<T>, IDisposable
        where T : ILexeme
    {
        #region Private Mambers
        List<T> lexemeList;
        int? constKey;
        int? idKey;
        int? labelKey;
        #endregion

        #region Properties
        public int ConstKey
        {
            get
            {
                if (constKey == null)
                    throw new Exception(
                        "Таблица не содержит определения константы.");
                return (int)constKey;
            }
        }
        public int IdKey
        {
            get
            {
                if (idKey == null)
                    throw new Exception(
                        "Таблица не содержит определения идентификатора.");
                return idKey.Value;
            }
        }
        public int LabelKey
        {
            get
            {
                if (labelKey == null)
                    throw new Exception(
                        "Таблица не содержит определения метки.");
                return (int)labelKey;
            }
        }
        public int Count
        {
            get { return lexemeList.Count; }
        }
        #endregion

        #region Constructors
        public LexemeTable()
        {
            lexemeList = new List<T>();
        }
        public LexemeTable(IEnumerable<T> list, 
            int idKey = -1, int constKey = -1, int labelKey = -1)
        {
            lexemeList = new List<T>(list);
            if(constKey > -1 && constKey < lexemeList.Count)
                this.constKey = constKey;
            if (idKey > -1 && idKey < lexemeList.Count)
                this.idKey = idKey;
            if (labelKey > -1 && labelKey < lexemeList.Count)
                this.labelKey = labelKey;
        }
        #endregion

        #region Public Methods
        public void Add(T obj)
        {
            lexemeList.Add(obj);
        }
        public ILexeme FindByName(string name)
        {
            return lexemeList.Find(x => x.Name == name);
        }
        public ILexeme FindLexeme(string var)
        {
            return lexemeList.Find(x => x.IsMatch(var));
        }
        #endregion

        #region IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)lexemeList).GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)lexemeList).GetEnumerator();
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).                   
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                // TODO: задать большим полям значение NULL.
                lexemeList = null;

                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        // ~LexemeTable() {
        //   // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
        //   Dispose(false);
        // }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public T this[int index]
        {
            get { return lexemeList[index]; }
        }

        internal LexemeTable<T> Clone()
        {
            LexemeTable<T> obj = (LexemeTable<T>)this.MemberwiseClone();
            obj.lexemeList = new List<T>(this.lexemeList);
            if(this.idKey != null)
                obj.idKey = new int?(this.idKey.Value);
            if (this.constKey != null)
                obj.constKey = new int?(this.constKey.Value);
            if (this.labelKey != null)
                obj.labelKey = new int?(this.labelKey.Value);
            return obj;
        }
    }
}
