using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using GdMMVC.Models;
using Microsoft.AspNetCore;

namespace GdMMVC.Controllers
{
    public class MaterialController : Controller
    {
        public string uriBase = "http://GdMEtec.somee.com/GDMAPI/Material/";


        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                string uriComplementar = "GetAll";
                HttpClient httpClient = new HttpClient();

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<MaterialViewModel> listaMateriais = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<MaterialViewModel>>(serialized));

                    return View(listaMateriais);
                }
                else
                    throw new SystemException(serialized);

            }
            catch(System.Exception ex)
            {
                TempData["MensagemErro"] =  ex.Message;
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public async Task<ActionResult> EditAsync(int? id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());

                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MaterialViewModel m = await Task.Run(() =>
                        JsonConvert.DeserializeObject<MaterialViewModel>(serialized));
                    return View(m);
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditAsync(MaterialViewModel m)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(m));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await httpClient.PutAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] =
                        string.Format("{0} de {1} atualizado com sucesso!", m.Tipo, m.Nome);
                    return RedirectToAction("Index");
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync(MaterialViewModel m)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(m));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("{0}, Id {1} salvo com sucesso!", m.Tipo, serialized);
                    return RedirectToAction("Index");
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                HttpResponseMessage response = await httpClient.DeleteAsync(uriBase + id.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] =
                        string.Format("Material com ID: {0} removido com sucesso!", id);
                    return RedirectToAction("Index");
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }



    }
}