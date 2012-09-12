using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Charltone.Domain;
using Charltone.Repositories;
using System.Web.Mvc;

namespace Charltone.ViewModels.Orderings
{
    public class OrderingEditViewModel
    {
        private readonly ITypeRepository _types;

        public OrderingEditViewModel()
        {
        }

        public OrderingEditViewModel(Ordering ordering, ITypeRepository types)
        {
            _types = types;

            Id = ordering.Id;
            Model = ordering.Model;
            TypicalPrice = ordering.TypicalPrice;
            Comments = ordering.Comments;

            if (ordering.InstrumentType == null) ordering.InstrumentType = new InstrumentType();
            InstrumentTypes = new SelectList(_types.GetInstrumentTypeList(), "Id", "InstrumentTypeDesc", ordering.InstrumentType.Id);
            InstrumentTypeId = ordering.InstrumentType.Id;

            if (ordering.Classification == null) ordering.Classification = new Classification();
            ClassificationTypes = new SelectList(_types.GetClassificationList(), "Id", "ClassificationDesc", ordering.Classification.Id);
            ClassificationId = ordering.Classification.Id;

            if (ordering.SubClassification == null) ordering.SubClassification = new SubClassification();
            SubClassificationTypes = new SelectList(_types.GetSubClassificationList(), "Id", "SubClassificationDesc", ordering.SubClassification.Id);
            SubClassificationId = ordering.SubClassification.Id;

        }

        public int Id;

        [UIHint("SelectList")]
        [DisplayName("Instrument Type")]
        public SelectList InstrumentTypes { get; set; }

        [UIHint("SelectList")]
        [DisplayName("Classification")]
        public SelectList ClassificationTypes { get; set; }

        [UIHint("SelectList")]
        [DisplayName("Style")]
        public SelectList SubClassificationTypes { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Description cannot exceed 25 characters")]
        public string Model { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        [DisplayName("Typical Price")]
        public string TypicalPrice { get; set; }

        [StringLength(1000, ErrorMessage = "Comments cannot exceed 1000 characters")]
        public string Comments { get; set; }

        public int InstrumentTypeId { get; set; }
        public int ClassificationId { get; set; }
        public int SubClassificationId { get; set; }
        public byte[] Photo { get; set; }
    }
}
