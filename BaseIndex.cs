//   Fatum Analytics -- Performance Analytics for Fatum
//
//   Copyright (C) 2003-2023 Eric Knight
//   This software is distributed under the GNU Public v3 License
//
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.

//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.

//   You should have received a copy of the GNU General Public License
//   along with this program.  If not, see <https://www.gnu.org/licenses/>.

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace Proliferation.FatumAnalytics
{
    public sealed class BaseIndex
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
