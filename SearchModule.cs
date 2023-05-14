using System.Collections;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace FatumAnalytics
{
    public class SearchModule
    {
        public ArrayList activeIndexes = new ArrayList();

        public void NewIndex(string directory, string filename)
        {
            
            Lucene.Net.Store.Directory indexdirectory = FSDirectory.Open(new DirectoryInfo(directory + "\\" + filename));
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            IndexWriter indexer = new IndexWriter(indexdirectory, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);
            activeIndexes.Add(indexer);
        }

        public void AdvanceIndexOnePeriod()
        {

        }

        public void CloseIndex(IndexWriter indexer)
        {
            indexer.Commit();
        }

    }
}
