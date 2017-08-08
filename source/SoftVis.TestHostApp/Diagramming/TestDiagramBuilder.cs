﻿using System;
using System.Collections.Generic;
using Codartis.SoftVis.Diagramming;
using Codartis.SoftVis.Diagramming.Implementation;
using Codartis.SoftVis.TestHostApp.Modeling;
using Codartis.SoftVis.TestHostApp.TestData;

namespace Codartis.SoftVis.TestHostApp.Diagramming
{
    internal class TestDiagramBuilder : DiagramBuilder
    {
        private static readonly Dictionary<Type, ConnectorType> ModelRelationshipTypeToConnectorTypeMap = new Dictionary<Type, ConnectorType>
        {
            {typeof(TestInheritanceRelationship), ConnectorTypes.Generalization},
            {typeof(TestImplementsRelationship), TestConnectorTypes.Implementation},
        };

        public override ConnectorType GetConnectorType(Type modelRelationshipType)
        {
            if (!ModelRelationshipTypeToConnectorTypeMap.ContainsKey(modelRelationshipType))
                throw new Exception($"Unexpected model relationship type {modelRelationshipType.Name}");

            return ModelRelationshipTypeToConnectorTypeMap[modelRelationshipType];
        }
    }
}
