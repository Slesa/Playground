using System.Collections.Generic;
using System.Xml.Linq;
using Machine.Specifications;

namespace Godot.IcsRunner.Core
{
    /* 
    <?xml version="1.0" encoding="utf-8" ?>
    <orders>
        <order>
            <count>4.0</count>
            <plu>42</plu>
            <price>4242</price>
        </order>
    </orders>
    */


    public static class JobReaderTestExtension
    {
        internal static XElement AddNode(this XElement root, XElement node)
        {
            root.Add(node);
            return root;
        }
        internal static XElement WithPlu(this XElement node, int plu)
        {
            node.Add(new XElement("plu", plu));
            return node;
        }
        internal static XElement WithCount(this XElement node, decimal count)
        {
            node.Add(new XElement("count", count.ToString("G", System.Globalization.CultureInfo.InvariantCulture)));
            return node;
        }
        internal static XElement WithPrice(this XElement node, int price)
        {
            node.Add(new XElement("price", price));
            return node;
        }
        internal static XElement WithCostcenter(this XElement node, int costcenter)
        {
            node.Add(new XElement("costcenter", costcenter));
            return node;
        }
    }

    public class JobReaderTestCore
    {
        internal static XElement GetRootElement() { return new XElement("orders"); }
        internal static XElement NewOrder() { return new XElement("order"); }
        internal static XElement NewVoid() { return new XElement("void"); }
    }

    [Subject(typeof(JobReader))]
    public class When_reading_empty_job : JobReaderTestCore
    {
        Establish context = () =>
        {
            _job = GetRootElement();
        };

        Because of = () => { _resolvedJobs = JobReader.ResolveJobs(_job); };
        It should_have_no_jobs = () => _resolvedJobs.Count.ShouldEqual(0);

        static XElement _job;
        static List<RecipeJob> _resolvedJobs;
    }

    [Subject(typeof(JobReader))]
    public class When_reading_only_empty_orders_and_voids : JobReaderTestCore
    {
        Establish context = () =>
        {
            _job = GetRootElement()
                .AddNode(NewOrder().WithCount(0.0m).WithPlu(42).WithPrice(240))
                .AddNode(NewOrder().WithCount(0.0m).WithPlu(44).WithPrice(720))
                .AddNode(NewVoid().WithCount(5.0m).WithPlu(44).WithPrice(142))
                .AddNode(NewOrder().WithCount(0.0m).WithPlu(46).WithPrice(144));
        };

        Because of = () => { _resolvedJobs = JobReader.ResolveJobs(_job); };
        It should_have_no_jobs = () => _resolvedJobs.Count.ShouldEqual(0);

        static XElement _job;
        static List<RecipeJob> _resolvedJobs;
    }

    [Subject(typeof(JobReader))]
    public class When_reading_single_order : JobReaderTestCore
    {
        Establish context = () =>
        {
            _job = GetRootElement().AddNode(NewOrder().WithCount(3.33m).WithPlu(42).WithPrice(242).WithCostcenter(43));
        };

        Because of = () => { _resolvedJobs = JobReader.ResolveJobs(_job); };

        It should_have_one_job = () => _resolvedJobs.Count.ShouldEqual(1);

        It should_have_correct_job = () =>
            {
                var job = _resolvedJobs[0];
                job.Quantity.ShouldEqual(3.33m);
                job.SalesItem.ShouldEqual(42);
                job.Costcenter.ShouldEqual(43);
            };

        static XElement _job;
        static List<RecipeJob> _resolvedJobs;
    }

    [Subject(typeof(JobReader))]
    public class When_reading_job : JobReaderTestCore
    {
        Establish context = () =>
            {
                _job = GetRootElement()
                    .AddNode(NewOrder().WithCount(1.0m).WithPlu(42).WithPrice(242))
                    .AddNode(NewOrder().WithCount(0.0m).WithPlu(44).WithPrice(142))
                    .AddNode(NewOrder().WithCount(2.25m).WithPlu(46).WithPrice(144))
                    .AddNode(NewVoid().WithCount(100.0m).WithPlu(44).WithPrice(142))
                    .AddNode(NewOrder().WithCount(2.25m).WithPlu(46).WithPrice(144));
            };

        Because of = () => { _resolvedJobs = JobReader.ResolveJobs(_job); };
        It should_ignore_zero_counters = () => _resolvedJobs.Count.ShouldEqual(3);

        static XElement _job;
        static List<RecipeJob> _resolvedJobs;
    }
}