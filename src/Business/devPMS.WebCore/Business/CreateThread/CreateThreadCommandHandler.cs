using University;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using SSTVN.PMS.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace devPMS.WebCore.Business
{
    public class CreateThreadCommandHandler : ICommandHandler<CreateThreadCommand>
    {
        private readonly IDbContextFactory _factory;

        public CreateThreadCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        private List<Tag> GetTag(List<string> tags, IUnitOfWork uow)
        {
            var tagRepo = uow.Repository<Tag>();
            List<Tag> tmpTags = new List<Tag>();
            List<Tag> tmpNewTags = new List<Tag>();

            foreach (var t in tags)
            {
                int num;
                if (int.TryParse(t, out num))
                {
                    // It's a id then get Tag
                    var foundTag = tagRepo.Get(num);
                    if (foundTag != null)
                    {
                        tmpTags.Add(foundTag);
                        continue;
                    }
                }
                // create for new tags
                var tag = new Tag();
                var convertTagName = Regex.Replace(t, "new:", "").Trim();
                tag.TagName = convertTagName;
                tag.UrlSeo = MixFunctions.ConvertURLSEO(convertTagName);
                tag.Checked = null;
                //
                tmpNewTags.Add(tag);
                
            }
            if (tmpNewTags.Count > 0)
            {
                tagRepo.AddRange(tmpNewTags);
                uow.SubmitChanges();
                // add into tmp after getting an id
                tmpTags.AddRange(tmpNewTags);
            }
            return tmpTags;
        }

        public void Handle(CreateThreadCommand command)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                try
                {
                    var thread = new Thread();
                    thread.UserId = command.UserId;
                    thread.CategoryId = command.CategoryId;
                    thread.Title = command.Title;
                    thread.ShortDescription = command.ShortDescription;
                    thread.Body = command.Body;
                    thread.UrlSeo = MixFunctions.ConvertURLSEO(command.Title);
                    thread.Meta = thread.UrlSeo; // use it at this time
                    thread.IsPublished = command.IsPublished;
                    thread.Image = command.Image;
                    thread.CreatedDT = DateTime.Now;
                    thread.ModifiedDT = DateTime.Now;
                    thread.View = 1; // Initialize
                    //tags 
                    foreach (var t in GetTag(command.Tags, uow))
                    {
                        thread.Tags.Add(t);
                    }
                    //
                    uow.Repository<Thread>().Add(thread);
                    uow.SubmitChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
            }
        }
    }
}