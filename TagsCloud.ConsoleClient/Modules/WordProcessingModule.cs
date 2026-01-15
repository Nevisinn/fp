using Autofac;
using TagsCloud.Core.Selectors;
using TagsCloud.Core.Services.WordsProcessing.DocumentWriters;
using TagsCloud.Core.Services.WordsProcessing.FileValidator;
using TagsCloud.Core.Services.WordsProcessing.WordsHandlers;
using TagsCloud.Core.Services.WordsProcessing.WordsPreprocessors;
using TagsCloud.Core.Services.WordsProcessing.WordsProviders;

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