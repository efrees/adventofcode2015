use adventlib;
use adventlib::grid::*;
use regex::Regex;

pub fn solve() {
    println!("Day 23");

    let lines = adventlib::read_input_lines("day23input.txt");

    let bots: Vec<_> = lines.iter().map(|l| parse_bot(l)).collect();
    let strongest_bot = bots.iter().max_by_key(|b| b.radius).expect("Must be a max");

    let bots_in_reach = bots_in_reach_of(strongest_bot, &bots);

    println!("Bots reached by strongest: {}", bots_in_reach);

    let origin = Point3d::new(0, 0, 0);
    let candidate_points = get_intersections(&bots);

    let mut max_bot_count = 0;
    let mut max_bot_dist = !0;
    let mut max_point = Point3d::origin();

    for point in candidate_points {
        let bot_count = reached_by_bot_count(&point, &bots);
        if bot_count >= max_bot_count {
            let bot_dist = point.manhattan_dist_to(&origin);

            if bot_count > max_bot_count || bot_dist < max_bot_dist {
                max_bot_count = bot_count;
                max_bot_dist = bot_dist;
                max_point = point;
            }
        }
    }

    // Maximally connected point: Point3d { x: 16483569, y: 49255058, z: 47259007 }
    // Maximally connected count: 928
    // Distance to maximally connected: 112997634
    println!("Maximally connected count: {}", max_bot_count);
    println!("Distance to maximally connected: {}", max_bot_dist);
}

fn parse_bot(line: &str) -> NanoBot {
    lazy_static! {
        static ref PATTERN: Regex =
            Regex::new(r"pos=<([-\d]+),([-\d]+),([-\d]+)>, r=(\d+)").expect("Parse pattern");
    }
    let captures = PATTERN.captures(line).expect("Line should match format");
    let location = Point3d::new(
        captures[1].parse().unwrap(),
        captures[2].parse().unwrap(),
        captures[3].parse().unwrap(),
    );
    NanoBot {
        location: location,
        radius: captures[4].parse().unwrap(),
    }
}

fn bots_in_reach_of(bot: &NanoBot, all_bots: &Vec<NanoBot>) -> usize {
    all_bots
        .iter()
        .filter(|b| b.location.manhattan_dist_to(&bot.location) <= bot.radius as i64)
        .count()
}

fn get_intersections(bots: &Vec<NanoBot>) -> Vec<Point3d> {
    // The space covered by each bot is a regular octahedron.
    // We don't have to restrict the points fully to the intersections within a face.
    // It is enough to have a reasonable, inclusive set of candidates.
    let mut intersections = Vec::<Point3d>::new();
    let plane_normals = vec![
        Point3d::new(1, 1, 1),
        Point3d::new(1, 1, -1),
        Point3d::new(1, -1, 1),
        Point3d::new(1, -1, -1),
        Point3d::new(-1, 1, 1),
        Point3d::new(-1, 1, -1),
        Point3d::new(-1, -1, 1),
        Point3d::new(-1, -1, -1),
    ];
    for bot in bots {
        let radius: i64 = bot.radius.into();
        let top_point = bot.location.add(&Point3d::new(0, radius, 0));
        let bottom_point = bot.location.add(&Point3d::new(0, -radius, 0));
        let middle_points = vec![
            bot.location.add(&Point3d::new(radius, 0, 0)),
            bot.location.add(&Point3d::new(-radius, 0, 0)),
            bot.location.add(&Point3d::new(0, 0, radius)),
            bot.location.add(&Point3d::new(0, 0, -radius)),
        ];

        for bot2 in bots {
            for plane_normal in plane_normals.iter() {
                let plane_point_x = i64::from(bot2.radius) * plane_normal.x;
                let plane_point = bot2.location.add(&Point3d::new(plane_point_x, 0, 0));
                for i in 0..middle_points.len() {
                    let line_end = &middle_points[i];
                    add_intersection_with_plane(
                        &top_point,
                        line_end,
                        &plane_point,
                        plane_normal,
                        radius,
                        &mut intersections,
                    );
                    add_intersection_with_plane(
                        &bottom_point,
                        line_end,
                        &plane_point,
                        plane_normal,
                        radius,
                        &mut intersections,
                    );
                    let next_index = (i + 1) % middle_points.len();
                    add_intersection_with_plane(
                        &middle_points[next_index],
                        line_end,
                        &plane_point,
                        plane_normal,
                        radius,
                        &mut intersections,
                    );
                }
            }
        }
    }

    return intersections;
}

fn add_intersection_with_plane(
    line_start: &Point3d,
    line_end: &Point3d,
    plane_point: &Point3d,
    plane_normal: &Point3d,
    line_radius: i64,
    intersections: &mut Vec<Point3d>,
) {
    // intuition: get magnitude of perpendicular component of a known vector reaching the plane
    // then divide by the magnitude of the perpendicular component of the line direction
    let l_vec = line_end.subtract(line_start).divide(line_radius);
    let perpendicular_magnitude_of_l_vec = l_vec.dot_product(&plane_normal);
    if perpendicular_magnitude_of_l_vec == 0 {
        return; // line parallel to plane
    }

    let distance = plane_point.subtract(&line_start).dot_product(&plane_normal)
        / perpendicular_magnitude_of_l_vec;

    if distance < 0 || distance > line_radius + line_radius / 2 {
        return; // beyond the reach of the line segment
    }

    let candidate = line_start.add(&l_vec.multiply(distance));

    if (plane_normal.x < 0 && candidate.x < plane_point.x)
        || (plane_normal.x > 0 && candidate.x > plane_point.x)
    {
        return; //trivially outside the plane
    }

    intersections.push(candidate);
}

fn reached_by_bot_count(location: &Point3d, all_bots: &Vec<NanoBot>) -> usize {
    all_bots
        .iter()
        .filter(|b| b.location.manhattan_dist_to(&location) <= b.radius as i64)
        .count()
}

struct NanoBot {
    location: Point3d,
    radius: u32,
}
