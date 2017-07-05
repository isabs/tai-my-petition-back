using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using WcfJsonRestService.Model;

namespace WcfJsonRestService
{
    public static class Translator
    {
        //From DB to Web

        public static WebModel.Person ToWeb ( this Model.Person modelPerson )
        {
            return new WebModel.Person ()
            {
                Name = modelPerson.Name,
                Id = modelPerson.PersonId,
                FbId = modelPerson.FacebookId
            };
        }

        public static List<WebModel.Person> ToWeb ( this ICollection<Model.Person> modelPerson )
        {
            return modelPerson.Select ( person => person.ToWeb () ).ToList ();
        }

        public static List<string> ToWeb ( this ICollection<Model.Tag> tags )
        {
            return tags.Select ( tag => tag.TagText ).ToList ();
        }

        public static WebModel.PetitionShort ToShortWeb ( this Model.Petition modelPetition )
        {
            return new WebModel.PetitionShort
            {
                Id = modelPetition.PetitionId,
                Tags = modelPetition.Tags.ToWeb (),
                Title = modelPetition.Title,
                CreationDate = modelPetition.CreationDate,
                SignCount = modelPetition.Members.Count,
                Owner = modelPetition.Creator.ToWeb (),
                Addressee = modelPetition.Addressee
            };
        }

        public static WebModel.PetitionNormal ToNormalWeb ( this Model.Petition modelPetition )
        {
            return new WebModel.PetitionNormal
            {
                Id = modelPetition.PetitionId,
                Tags = modelPetition.Tags.ToWeb (),
                Title = modelPetition.Title,
                CreationDate = modelPetition.CreationDate,
                SignCount = modelPetition.Members.Count,
                Owner = modelPetition.Creator.ToWeb (),
                Text = modelPetition.Text,
                ImageBase64 = modelPetition.Url,
                Signs = modelPetition.Members.ToWeb (),
                Addressee = modelPetition.Addressee,
                Description = modelPetition.Description
            };
        }

        public static List<WebModel.PetitionShort> ToWeb ( this ICollection<Model.Petition> modelPetitions )
        {
            return modelPetitions.Select ( petition => petition.ToShortWeb () ).ToList ();
        }

        // From Web to DB

        public static List<Model.Tag> ToDbModel ( this ICollection<string> tags )
        {
            return tags.Select ( tag => new Model.Tag { TagText = tag } ).ToList ();
        }

        public static Model.Person ToDbPerson ( this WebModel.Person p )
        {
            return new Person ()
            {
                FacebookId = p.FbId,
                Name = p.Name
            };
        }

        public static Model.Petition ToDbPetition ( this WebModel.PetitionNormal web )
        {
            return new Petition ()
            {
                Title = web.Title,
                Text = web.Text,
                Url = web.ImageBase64,
                Addressee = web.Addressee,
                Description = web.Description,
                CreatorId = web.Owner.Id,
                CreationDate = web.CreationDate,
                IsValid = true,

                Tags = web.Tags.ToDbModel (),
                Creator = web.Owner.ToDbPerson (),
                Members = web.Signs.Select ( person => person.ToDbPerson () ).ToList ()
            };
        }
    }
}