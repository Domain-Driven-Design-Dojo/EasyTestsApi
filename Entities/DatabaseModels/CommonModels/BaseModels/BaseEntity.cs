using Entities.DatabaseModels.UserModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DatabaseModels.CommonModels.BaseModels
{
    public interface IEntity
    {
    }

    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
    }

    public interface IBaseEntityWithActors<TKey> : IEntity<TKey>
    {

        public bool IsActive { get; set; }

        public long CreatorId { get; set; }

        public long ModifierId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        //[ForeignKey("CreatorId")]
        //public ApplicationUser Creator { get; set; }

        //[ForeignKey("ModifierId")]

        //public ApplicationUser Modifier { get; set; }
    }

    public abstract class BaseEntity<TKey> : IEntity<TKey>
    {
        public virtual TKey Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }

    public abstract class BaseEntityWithActors<TKey> : BaseEntity<TKey>, IBaseEntityWithActors<TKey>
    {
        [Required]
        public bool IsActive { get; set; }

        [Required]
        public long CreatorId { get; set; }

        [Required]
        public long ModifierId { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime ModificationDate { get; set; }

        //[ForeignKey("CreatorId")]
        //[Required]
        //public ApplicationUser Creator { get; set; }

        //[ForeignKey("ModifierId")]
        //[Required]
        //public ApplicationUser Modifier { get; set; }
    }

    public abstract class BaseEntityWithActorsNoIdentity : BaseEntityWithActors<int>
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }
    }
    public abstract class BaseEntityWithActorsNoIdentityLong : BaseEntityWithActors<long>
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override long Id { get; set; }
    }
}
