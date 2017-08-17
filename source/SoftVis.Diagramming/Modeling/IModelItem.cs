﻿namespace Codartis.SoftVis.Modeling
{
    /// <summary>
    /// A constituent of a model.
    /// </summary>
    public interface IModelItem
    {
        /// <summary>
        /// The Id of a model item does not change during its lifetime.
        /// </summary>
        ModelItemId Id { get; }
    }
}