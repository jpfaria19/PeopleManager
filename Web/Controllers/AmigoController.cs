using BLL.Domain;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Controllers
{
    public class AmigoController : Controller
    {
        AmigoRepository amigoRepository = new AmigoRepository();

        // GET: Amigo
        public ActionResult Index()
        {
            return View();
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (Session["CheckAmigoID"] == null)
                Session["CheckAmigoID"] = new List<int>();
        }

        // GET: Checkbox
        public ActionResult MarkCheckBox(int amigoId)
        {
            List<int> CheckAmigoID = (List <int>) Session["CheckAmigoID"];
            CheckAmigoID.Add(amigoId);
            Session["CheckAmigoID"] = CheckAmigoID;

            return Json(new { status = "OK" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MarkOffCheckBox(int amigoId)
        {
            List<int> RemoveCheckBox = (List<int>)Session["CheckAmigoID"];
            RemoveCheckBox.Remove(amigoId);
            Session["CheckAmigoID"] = RemoveCheckBox;

            return Json(new { status = "OK" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List_date()
        {
            List<Amigo> mahFriends = (List<Amigo>) amigoRepository.GetAllAmigos();
            List<int> markedFriends = (List<int>) Session["CheckAmigoID"];

            foreach (var item in markedFriends)
                mahFriends.Find(mf => mf.Id == item).CheckBoxAmigo = true;

            return View(mahFriends);
        }

        public ActionResult List_email()
        {
            List<Amigo> mahFriends = (List<Amigo>)amigoRepository.GetAllAmigos();
            List<int> markedFriends = (List<int>)Session["CheckAmigoID"];

            foreach (var item in markedFriends)
                mahFriends.Find(mf => mf.Id == item).CheckBoxAmigo = true;

            return View(mahFriends);
        }

        // GET: Amigo/Details/5
        public ActionResult Details(int id)
        {
            return View(amigoRepository.GetAmigo(id));
        }

        // GET: Amigo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Amigo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            amigoRepository.AddAmigo(collection["nome"], collection["sobreNome"], collection["email"], DateTime.Parse(collection["nascimento"]));

            return RedirectToAction("Index");
        }

        // GET: Amigo/Edit/5
        public ActionResult Edit(int id)
        {
            return View(amigoRepository.GetAmigo(id));
        }

        // POST: Amigo/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            Amigo amigo = new Amigo()
            {
                Id = int.Parse(formCollection["id"]),
                Nome = formCollection["nome"],
                SobreNome = formCollection["sobreNome"],
                Email = formCollection["email"],
                Nascimento = DateTime.Parse(formCollection["nascimento"])
            };

            amigoRepository.UpdateAmigo(amigo);

            return RedirectToAction("Index");
        }

        // GET: Amigo/Delete/5
        public ActionResult Delete(int id)
        {
            amigoRepository.RemoveAmigo(id);

            return RedirectToAction("Index");
        }

        // POST: Amigo/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
