using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace FatumAnalytics
{
    public class BaseIndex
    {
        public string directory = "";
        public IndexWriter indexer = null;
        public Analyzer analyzer = null;
        public Lucene.Net.Store.Directory indexdirectory = null;

        public BaseIndex(string Directory)
        {
            directory = Directory;
            
            indexdirectory = FSDirectory.Open(new DirectoryInfo(Directory));
            analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            try
            {
                indexer = new IndexWriter(indexdirectory, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);
            }
            catch (Exception)
            {
                // cannot open database, probably because its already been opened for today
                indexer = null;
            }
        }

        public void Close()
        {
            if (indexer != null)
            {
                indexer.Dispose();
                indexer = null;
            }

            if (analyzer!= null)
            {
                analyzer.Close();
                analyzer.Dispose();
                analyzer = null;
            }
        }
    }
}
