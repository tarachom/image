using System;
using System.Collections.Generic;

namespace ImageLibrary
{
    /// <summary>
    /// Варіанти обєднання
    /// </summary>
    public enum PicturesUnionType
    {
        /// <summary>
        /// Пункт А трансформувати в пункт Б
        /// </summary>
        PuncktA_transform_to_PuncktB = 1,

        /// <summary>
        /// Пункт А вкласти в пункт Б
        /// </summary>
        PuncktA_in_to_PuncktB = 2
    }

    public class PicturesUnionTrack
    {
        private Pictures m_Parent;
        private PicturesBase m_PuncktA;
        private PicturesBase m_PuncktB;
        private PicturesUnionType m_UnionType;
        private ImageContext m_ContextA;
        private ImageContext m_ContextB;
        private ImageBase m_ImageField;

        public Pictures Parent
        {
            get
            {
                return m_Parent;
            }

            set
            {
                m_Parent = value;
            }
        }

        public PicturesBase PuncktA
        {
            get
            {
                return m_PuncktA;
            }

            set
            {
                m_PuncktA = value;
            }
        }

        public PicturesBase PuncktB
        {
            get
            {
                return m_PuncktB;
            }

            set
            {
                m_PuncktB = value;
            }
        }

        public PicturesUnionType UnionType
        {
            get
            {
                return m_UnionType;
            }

            set
            {
                m_UnionType = value;
            }
        }

        public ImageContext ContextA
        {
            get
            {
                return m_ContextA;
            }

            set
            {
                m_ContextA = value;
            }
        }

        public ImageContext ContextB
        {
            get
            {
                return m_ContextB;
            }

            set
            {
                m_ContextB = value;
            }
        }

        public ImageBase ImageField
        {
            get
            {
                return m_ImageField;
            }

            set
            {
                m_ImageField = value;
            }
        }
    }
}
