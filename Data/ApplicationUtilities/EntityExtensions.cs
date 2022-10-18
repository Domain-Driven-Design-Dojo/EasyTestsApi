using System;
using System.Collections.Generic;
using System.Linq;
using Entities.DatabaseModels.CommonModels.BaseModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Data.ApplicationUtilities
{
    public static class EntityExtensions
    {
        //public static object AddCreator<T>(this ActorInformationModel<T> input, long creatorId)
        //{
        //    input.IsActive = true;
        //    input.CreatorId = creatorId;
        //    input.ModifierId = creatorId;
        //    input.CreationDate = DateTime.Now;
        //    input.ModificationDate = DateTime.Now;

        //    return input;
        //}

        public static void AddCreator<T, TKey>(this T input, long creatorId) where T : BaseEntityWithActors<TKey>
        {
            input.IsActive = true;
            input.CreatorId = creatorId;
            input.ModifierId = creatorId;
            input.CreationDate = DateTime.Now;
            input.ModificationDate = DateTime.Now;
        }

        public static void AddModifier<T, TKey>(this T input, long modifierId) where T : BaseEntityWithActors<TKey>
        {
            input.ModifierId = modifierId;
            input.ModificationDate = DateTime.Now;
        }
    }
}
