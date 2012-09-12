using Charltone.Domain;
using NHibernate;

namespace Charltone.Controllers
{
    using System.Web.Mvc;
    using Repositories;

    public class GalleryController : Controller
    {
        private readonly IInstrumentRepository _instruments;
        private readonly ISession _session;

        public GalleryController(IInstrumentRepository instruments, ISession session)
        {
            _instruments = instruments;
            _session = session;
        }

        public ActionResult Index(int? page)
        {
            var instrumentList = _instruments.GetList(page.GetValueOrDefault(1), Request.IsAuthenticated);
            return View(instrumentList);
        }

        public ActionResult Photo(int? page, int id)
        {
            var instrument = _instruments.GetSingle(id);
            return View(instrument);
        }

        public FileResult GetPhoto(int id)
        {
            byte[] photo = _session.Load<Photo>(id).Data;
            return File(photo, "image/jpeg");
        }
    }
}
