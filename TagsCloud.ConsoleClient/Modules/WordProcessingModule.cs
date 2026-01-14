using Autofac;
using TagsCloud.Infrastructure.Selectors;
using TagsCloud.Infrastructure.Services.WordsProcessing.DocumentWriters;
using TagsCloud.Infrastructure.Services.WordsProcessing.FileValidator;
using TagsCloud.Infrastructure.Services.WordsProcessing.WordsHandlers;
using TagsCloud.Infrastructure.Services.WordsProcessing.WordsPreprocessors;
using TagsCloud.Infrastructure.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Modules;

public class WordProcessingModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TxtWriter>().As<IDocumentWriter>();
        builder.RegisterType<DocWriter>().As<IDocumentWriter>();
        builder.RegisterType<DocxWriter>().As<IDocumentWriter>();

        builder.RegisterType<FileValidator>().As<IFileValidator>();

        builder.RegisterType<BoringWordsHandler>().As<IWordsHandler>();

        builder.RegisterType<DefaultWordsPreprocessor>().As<IWordsPreprocessor>();

        builder.RegisterType<TxtWordsProvider>().As<IWordsProvider>();
        builder.RegisterType<DocWordsProvider>().As<IWordsProvider>();
        builder.RegisterType<DocxWordsProvider>().As<IWordsProvider>();

        builder.RegisterType<WordsProviderSelector>().As<IWordsProviderSelector>();

        builder.RegisterType<DefaultWordsPreprocessor>().As<IWordsPreprocessor>();
    }
}