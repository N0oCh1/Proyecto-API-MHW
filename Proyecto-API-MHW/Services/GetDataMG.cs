//using Proyecto_API_MHW.DataClass;
//using Proyecto_API_MHW.Models;

//namespace Proyecto_API_MHW.Service
//{
//    public class GetDataMG
//    {

//        public List<DtomonstroGrande> response = new List<DtomonstroGrande>();
//        public List<MonstroGrande> monstroGrandes;
//        public List<Categoria> categorias;
//        public List<Bioma> biomas;
//        public List<MgBioma> mgBiomas;
//        public List<Rango> rangos;
//        public List<MgRango> mgRangos;
//        public List<Elemento> elementos;
//        public List<MgElemento> mgElementos;
//        public List<MgDebilidad> mgDebilidad;
//        public List<Item> items;

//        public GetDataMG(
//            List<MonstroGrande> monstroGrandes,
//            List<Categoria> categorias,
//            List<Bioma>  biomas,
//            List<MgBioma> mgBiomas,
//            List<Rango> rangos,
//            List<MgRango> mgRangos,
//            List<Elemento> elementos,
//            List<MgElemento> mgElementos,
//            List<MgDebilidad> mgDebilidad,
//            List<Item> items
//            ) 
//        {
//        // seteo valores
//            this.monstroGrandes = monstroGrandes;
//            this.categorias = categorias;
//            this.biomas = biomas;
//            this.mgBiomas = mgBiomas;
//            this.rangos = rangos;
//            this.mgRangos = mgRangos;
//            this.elementos = elementos;
//            this.mgElementos = mgElementos;
//            this.mgDebilidad = mgDebilidad;
//            this.items = items;
//        }
//        public List<DtomonstroGrande> GetMonstro()
//        {
//            foreach(MonstroGrande monstroGrande in monstroGrandes)
//            {

//                string tipo = "";
//                List<string> tempBioma = new List<string>();
//                List<DtoItem> tempItem = new List<DtoItem>();
//                List<Elemento> tempElemento = new List<Elemento>();
//                List<DtoDebilidad> tempDebilidad = new List<DtoDebilidad>();
//                List<string> tempRango = new List<string>();

//                // buscar la categoria que coinside con id_Monstro
//                categorias.ForEach(categoria =>
//                {
//                    if (categoria.id_tipo_monstro.Equals(monstroGrande.id_categoria))
//                    {
//                        tipo = categoria.tipo;
//                    }
//                });

//                // buscar los biomas que coinside con id_Monstro
//                // relacion mucho a mucho
//                mgBiomas.ForEach(mgBioma =>
//                {
//                    if (mgBioma.id_monstro.Equals(monstroGrande.id_monstrog))
//                    {
//                        biomas.ForEach(bioma =>
//                        {
//                            if (mgBioma.id_bioma.Equals(bioma.id_bioma))
//                            {
//                                tempBioma.Add(bioma.nombre_bioma);
//                            }
//                        });
//                    }
//                });
//                // buscar los rango del monstro que coinside con id_monstro
//                mgRangos.ForEach(mgRango => 
//                {
//                    if (mgRango.id_monstro.Equals(monstroGrande.id_monstrog))
//                    {
//                        rangos.ForEach(rango =>
//                        {
//                            if (rango.id_rango.Equals(mgRango.id_rango))
//                            {
//                                tempRango.Add(rango.rango);
//                            }
//                        });
//                    }
//                });
//                // buscar los elementos que coinside con id_monstro
//                mgElementos.ForEach(mgElemento =>
//                {
//                    if (mgElemento.id_monstro.Equals(monstroGrande.id_monstrog))
//                    {
//                        elementos.ForEach(elemento =>
//                        {
//                            if (mgElemento.id_elemento.Equals(elemento.id_elemento))
//                            {
//                                tempElemento.Add(elemento);
//                            }
//                        });
//                    }
//                });
//                // buscar las debilidades que coinside con id_monstro
//                mgDebilidad.ForEach(mgDebilidad => 
//                {
//                    if (mgDebilidad.id_monstro.Equals(monstroGrande.id_monstrog))
//                    {
//                        elementos.ForEach((elemento) => 
//                        {
//                            if (mgDebilidad.id_elemento.Equals(elemento.id_elemento))
//                            {
//                                tempDebilidad.Add(
//                                    new DtoDebilidad 
//                                    { 
//                                        id_elemento = elemento.id_elemento,
//                                        elemento = elemento.elemento,
//                                        eficacia = mgDebilidad.eficacia
//                                    }
//                                );
//                            }
//                        });
//                    }
//                });
//                // buscar los items que coinside con id_Monstro
//                // relacion uno a mucho
//                items.ForEach(item =>
//                {
//                    if(item.id_monstro.Equals(monstroGrande.id_monstrog))
//                    {
//                        tempItem.Add(new DtoItem { id = item.id_item, name = item.nombre_item, description = item.descripcion_item });
//                    }
//                });

//                // añado los valores obtenido a una lista para monstrar los datos que quiero
//                response.Add
//                (
//                    new DtomonstroGrande
//                    {
//                        id = monstroGrande.id_monstrog,
//                        name = monstroGrande.nombre,
//                        health = monstroGrande.vida,
//                        monsterClass = tipo,
//                        location = tempBioma,
//                        range = tempRango,
//                        elements = tempElemento,
//                        weekness = tempDebilidad,
//                        items = tempItem
//                    }
//                );
//            }  

//            return response;
//        }
//    }
//}
